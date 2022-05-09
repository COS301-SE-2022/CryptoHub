package com.api.cryptohub.api.controllers;

import com.api.cryptohub.domain.models.User;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.List;

import static com.api.cryptohub.mocks.UserMock.userMock;

@RestController
@RequestMapping(path="api/authorization")
public class AuthorizationController {

    @PostMapping(path = "login")
    public ResponseEntity<Response> Login(@RequestBody User user)
    {
        var loginUser = userMock.stream()
                .filter(u -> user.getEmail().equals(u.getEmail()))
                .findAny()
                .orElse(null);

        if(loginUser == null)
            return ResponseEntity.badRequest().body(new Response("incorrect username or password"));

        if(!loginUser.getPassword().equals(user.getPassword()))
            return ResponseEntity.badRequest().body(new Response("incorrect username or password"));

        return ResponseEntity.ok().body(new Response("logged in"));
    }

    @PostMapping("register")
    public ResponseEntity<Response> Register(@RequestBody User user)
    {
        var registerUser = userMock.stream()
                .filter(u -> user.getEmail().equals(u.getEmail()))
                .findAny()
                .orElse(null);

        if(registerUser != null)
            return ResponseEntity.badRequest().body(new Response("user already exists"));

        int newId = userMock.get(userMock.size()-1).getUserId()+1;
        user.setUserId(newId);
        userMock.add(user);

        return ResponseEntity.badRequest().body(new Response("registered"));
    }

    public class Response
    {
        private String response;
        public Response(String response)
        {
            this.response = response;
        }

        public String getResponse() {
            return response;
        }
    }



}

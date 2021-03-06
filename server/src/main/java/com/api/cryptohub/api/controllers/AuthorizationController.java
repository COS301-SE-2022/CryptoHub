package com.api.cryptohub.api.controllers;

import com.api.cryptohub.businesslogic.repositories.UserRepository;
import com.api.cryptohub.domain.models.User;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@CrossOrigin(origins="http://localhost:3000")
@RestController
@RequestMapping(path="api/authorization")
public class AuthorizationController {

    private final UserRepository userRepository;

    @Autowired
    public AuthorizationController(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    @PostMapping(path = "login")
    public ResponseEntity<Response> Login(@RequestBody User user)
    {
        var loginUser = userRepository.getUserByEmail(user.getEmail());

        if(loginUser == null)
            return ResponseEntity.badRequest().body(new Response("incorrect username or password",false,-1 ,""));

        if(!loginUser.getPassword().equals(user.getPassword()))
            return ResponseEntity.badRequest().body(new Response("incorrect username or password",false,-1,""));

        return ResponseEntity.ok().body(new Response("logged in",true,loginUser.getUserId(), loginUser.getUserName()));
    }

    @PostMapping("register")
    public ResponseEntity<Response> Register(@RequestBody User user)
    {
        var registerUser = userRepository.getUserByEmail(user.getEmail());

        if(registerUser != null)
            return ResponseEntity.badRequest().body(new Response("user already exists",false,-1,""));

        userRepository.save(user);

        return ResponseEntity.ok().body(new Response("registered",true,user.getUserId(), user.getUserName()));
    }

    public static class Response
    {
        private final String response;
        private final Boolean authorized;

        private final Integer userId;

        private final String username;
        public Response(String response, Boolean authtorized,Integer userId,String username)
        {
            this.response = response;
            this.authorized = authtorized;
            this.userId  = userId;
            this.username = username;
        }

        public String getResponse() {
            return response;
        }
        public Boolean getAuthorized(){return authorized;}

        public Integer getUserId() {
            return userId;
        }

        public String getUsername() {
            return username;
        }
    }



}

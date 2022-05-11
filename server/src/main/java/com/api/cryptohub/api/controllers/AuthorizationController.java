package com.api.cryptohub.api.controllers;

import com.api.cryptohub.businesslogic.repositories.UserRepository;
import com.api.cryptohub.domain.models.User;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import static com.api.cryptohub.mocks.UserMock.userMock;

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
            return ResponseEntity.badRequest().body(new Response("incorrect username or password",false ));

        if(!loginUser.getPassword().equals(user.getPassword()))
            return ResponseEntity.badRequest().body(new Response("incorrect username or password",false));

        return ResponseEntity.ok().body(new Response("logged in",true));
    }

    @PostMapping("register")
    public ResponseEntity<Response> Register(@RequestBody User user)
    {
        var registerUser = userRepository.getUserByEmail(user.getEmail());

        if(registerUser != null)
            return ResponseEntity.badRequest().body(new Response("user already exists",false));

        userRepository.save(user);

        return ResponseEntity.badRequest().body(new Response("registered",true));
    }

    public static class Response
    {
        private final String response;
        private final Boolean authorized;
        public Response(String response, Boolean authtorized)
        {
            this.response = response;
            this.authorized = authtorized;
        }

        public String getResponse() {
            return response;
        }
        public Boolean getAuthorized(){return authorized;}
    }



}

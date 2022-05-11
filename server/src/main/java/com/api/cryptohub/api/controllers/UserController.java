package com.api.cryptohub.api.controllers;

import com.api.cryptohub.businesslogic.repositories.UserRepository;
import com.api.cryptohub.domain.models.User;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

import static com.api.cryptohub.mocks.UserMock.userMock;

@RequestMapping(path = "api/user")
@RestController
public class UserController {

    private final UserRepository userRepository;

    @Autowired
    public UserController(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    @GetMapping("getallusers")
    public ResponseEntity<List<User>> getAllUsers()
    {

        var users = userRepository.findAll();
        return ResponseEntity
                .ok()
                .body(users);
    }

    @GetMapping(path = "{id}")
    public ResponseEntity<User> getUserById(@PathVariable("id") Integer id)
    {
        User user = userMock.stream()
                .filter(u -> id.equals(u.getUserId()))
                .findAny()
                .orElse(null);

        if(user==null)
            return ResponseEntity
                    .badRequest()
                    .body(null);

        return ResponseEntity.ok().body(user);

    }


}

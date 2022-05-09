package com.api.cryptohub.api.controllers;

import com.api.cryptohub.domain.models.User;
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

    @GetMapping("getallusers")
    public ResponseEntity<List<User>> getAllUsers()
    {
        return ResponseEntity
                .ok()
                .body(userMock);
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

package com.api.cryptohub.api.controllers;

import com.api.cryptohub.businesslogic.repositories.UserRepository;
import com.api.cryptohub.domain.models.User;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.ArrayList;
import java.util.List;

import static com.api.cryptohub.mocks.UserMock.userMock;

@CrossOrigin(origins="http://localhost:3000")
@RequestMapping(path = "api/user")
@RestController
public class UserController {

    private final UserRepository userRepository;

    @Autowired
    public UserController(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    @GetMapping("getallusers")
    public ResponseEntity<List<User>> getAllUsers() {

        var users = userRepository.findAll();
        return ResponseEntity
                .ok()
                .body(users);
    }

    @GetMapping(path = "{id}")
    public ResponseEntity<User> getUserById(@PathVariable("id") Integer id) {

        User user = userRepository.getById(id);

        if (user == null)
            return ResponseEntity
                    .badRequest()
                    .body(null);

        return ResponseEntity.ok().body(user);

    }

    @GetMapping(path = "getfollowing/{id}")
    public ResponseEntity<List<User>> getFollowing(@PathVariable("id") Integer id) {
        User user = userRepository.getById(id);
        if (user == null)
            return ResponseEntity
                    .badRequest()
                    .body(null);

        return ResponseEntity.ok().body(user.getFollowing());

    }

    @GetMapping(path = "getfollowers/{id}")
    public ResponseEntity<List<User>> getFollowers(@PathVariable("id") Integer id) {

        User user = userRepository.getById(id);
        if (user == null)
            return ResponseEntity
                    .badRequest()
                    .body(null);

        return ResponseEntity.ok().body(user.getFollowers());

    }

    @PostMapping(path = "follow")
    public ResponseEntity<String> followUser(@RequestBody FollowDTO followDTO) {

        if (followDTO.getFollowerId().equals(followDTO.getUserId()))
            return ResponseEntity
                    .badRequest()
                    .body("cannot follow self");

        User user = userRepository.getById(followDTO.getUserId());
        User follow = userRepository.getById(followDTO.getFollowerId());

        if (user == null)
            return ResponseEntity
                    .badRequest()
                    .body("user not found");


        if (follow == null)
            return ResponseEntity
                    .badRequest()
                    .body("target not found");

        var isFollowed = user.getFollowing()
                .stream()
                .filter(f -> f.getUserId().equals(follow.getUserId()))
                .findFirst()
                .orElse(null);

        if (isFollowed!=null)
            return ResponseEntity
                    .badRequest()
                    .body("already followings");


        //user.setFollowing(new ArrayList<>());
        user.addFollowing(follow);
        follow.addFollowers(user);

        userRepository.save(user);
        userRepository.save(follow);

        return ResponseEntity
                .ok()
                .body("following " + follow.getUserName());


    }

    public static class FollowDTO {
        private Integer userId;
        private Integer followerId;

        public FollowDTO() {
        }

        public Integer getUserId() {
            return userId;
        }

        public void setUserId(Integer userId) {
            this.userId = userId;
        }

        public Integer getFollowerId() {
            return followerId;
        }

        public void setFollowerId(Integer followerId) {
            this.followerId = followerId;
        }
    }


}

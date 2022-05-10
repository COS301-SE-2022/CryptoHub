package com.api.cryptohub.api.controllers;

import com.api.cryptohub.domain.models.Like;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;


@RestController
@RequestMapping(path="api/like")
public class LikeController {

    @GetMapping(path="getlikesforpost/{id}")
    public ResponseEntity<Like> GetLikesForPost(@PathVariable("id") Integer id)
    {
        return null;
    }

}

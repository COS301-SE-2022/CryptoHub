package com.api.cryptohub.api.controllers;

import com.api.cryptohub.domain.models.Like;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

import static com.api.cryptohub.mocks.LikeMock.likeMock;


@RestController
@RequestMapping(path="api/like")
public class LikeController {

    @PostMapping(path = "likepost")
    public ResponseEntity<Like> likePost(@RequestBody Like like)
    {
        var likedPost = likeMock
                        .stream()
                        .filter(l -> l.getUserId().equals(like.getUserId()) && l.getPostId().equals(like.getPostId()))
                        .findAny()
                        .orElse(null);

        if(likedPost==null)
        {
            like.setLiked(true);
            likeMock.add(like);
        }
        else
            likedPost.setLiked(!likedPost.getLiked());

        return ResponseEntity.ok().body(likedPost);
    }

}

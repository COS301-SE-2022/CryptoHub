package com.api.cryptohub.api.controllers;

import com.api.cryptohub.businesslogic.repositories.PostRepository;
import com.api.cryptohub.domain.models.Post;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

import static com.api.cryptohub.mocks.PostMock.postMock;

@RestController
@RequestMapping(path = "api/post")
public class PostController {

    private final PostRepository postRepository;

    @Autowired
    public PostController(PostRepository postRepository) {
        this.postRepository = postRepository;
    }

    @GetMapping("getallposts")
    public ResponseEntity<List<Post>> getAllPosts()
    {
        return  ResponseEntity
                .ok()
                .body(postRepository.findAll());
    }

    @GetMapping(path = "getpostsbyuser/{id}")
    public ResponseEntity<List<Post>> getPostsByUserId(@PathVariable("id") Integer id)
    {
        var post = postRepository.findPostsByUserId(id);

        return ResponseEntity
                .ok()
                .body(post);
    }

    @PostMapping("createpost")
    public ResponseEntity<Post> createPost(@RequestBody Post post)
    {
        Integer postId = postMock.get(postMock.size()-1).getPostId()+1;
        post.setPostId(postId);
        postMock.add(post);

        return ResponseEntity
                .ok()
                .body(post);
    }


}

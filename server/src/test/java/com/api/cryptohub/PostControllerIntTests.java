package com.api.cryptohub;

import com.api.cryptohub.businesslogic.repositories.PostRepository;
import com.api.cryptohub.businesslogic.repositories.UserRepository;
import com.api.cryptohub.domain.models.Post;
import com.api.cryptohub.domain.models.User;
import org.hamcrest.CoreMatchers;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.ResultActions;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;
import org.springframework.test.web.servlet.result.MockMvcResultMatchers;

import java.util.List;

@SpringBootTest
@AutoConfigureMockMvc
public class PostControllerIntTests {

    @Autowired
    private PostRepository postRepository;

    @Autowired
    private MockMvc mockMvc;

    //@Autowired
    //private UserRepository userRepository;

    @Test
    public void givenPost_whenGetAllPosts_thenListOfPosts() throws Exception
    {
        Post post = new Post();
        post.setUserid(1);
        post.setPost("This is a post");
        post.setPostId(100);
        User u = new User();
        u.setUserId(post.getUserid());
        post.setUser(u);

        postRepository.save(post);

        ResultActions response = mockMvc.perform(MockMvcRequestBuilders.get("/api/post/getallposts"));
        response.andExpect(MockMvcResultMatchers.status().isOk());
        response.andExpect(MockMvcResultMatchers.jsonPath("$.size()", CoreMatchers.is(postRepository.findAll().size())));
    }

//    @Test
//    public void givenUserId_whenGetPostByUserId_thenUserOfId() throws Exception
//    {
//
//        Post post = new Post();
//        post.setPost("This is a post");
//        post.setPostId(100);
//        User u = new User("John", "Smith", "Adress@gmail.com", "123", "user");
//        userRepository.save(u);
//        post.setUser(u);
//
//
//        int num = postRepository.findAll().size();
//        post.setUserid(num);
//
//        postRepository.save(post);
//        ResultActions response = mockMvc.perform(MockMvcRequestBuilders.get("/api/post/getpostsbyuser/" + num));
//
//        response.andExpect(MockMvcResultMatchers.status().isOk());
//        response.andExpect(MockMvcResultMatchers.jsonPath("$[0].post", CoreMatchers.is(post.getPost())));
//    }

//    @Test
//    public void givenPost_whencreatePost_thenPost() throws Exception
//    {
//        Post post = new Post();
//        post.setPost("This is a post");
//        post.setPostId(100);
//        User u = new User("John", "Smith", "Adress2@gmail.com", "123", "user2");
//        userRepository.save(u);
//        post.setUser(u);
//
//        ResultActions response = mockMvc.perform(MockMvcRequestBuilders.get("/api/post/createpost"));
//
//        response.andExpect(MockMvcResultMatchers.status().isOk());
//        response.andExpect(MockMvcResultMatchers.jsonPath("$.post", CoreMatchers.is(post.getPost())));
//    }
}

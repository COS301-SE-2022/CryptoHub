package com.api.cryptohub.api.controllers;

import com.api.cryptohub.domain.models.Like;
import com.api.cryptohub.domain.models.User;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.ArrayList;
import java.util.List;

import static com.api.cryptohub.mocks.LikeMock.likeMock;
import static com.api.cryptohub.mocks.PostMock.postMock;
import static com.api.cryptohub.mocks.UserMock.userMock;

@RestController
@RequestMapping(path = "api/feed")
public class FeedController {


    @GetMapping
    public ResponseEntity<List<Feed>> GetFeed()
    {
        List<Feed> feeds = new ArrayList<Feed>();

        for (var post:postMock)
        {
            User user = userMock.stream()
                    .filter(u -> post.getUserId().equals(u.getUserId()))
                    .findAny()
                    .orElse(null);

            long like = likeMock
                    .stream()
                    .filter(l -> l.getPostId().equals(post.getPostId())).count();

            feeds.add(new Feed(user.getUserName(),post.getPost(), (int) like));
        }

        return ResponseEntity.ok().body(feeds);
    }

    public class Feed
    {
        private String username;
        private String content;
        private Integer likes;

        public Feed(String username, String content, Integer likes) {
            this.username = username;
            this.content = content;
            this.likes = likes;
        }

        public String getUsername() {
            return username;
        }

        public void setUsername(String username) {
            this.username = username;
        }

        public String getContent() {
            return content;
        }

        public void setContent(String content) {
            this.content = content;
        }

        public Integer getLikes() {
            return likes;
        }

        public void setLikes(Integer likes) {
            this.likes = likes;
        }
    }


}

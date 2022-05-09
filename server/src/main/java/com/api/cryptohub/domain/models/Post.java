package com.api.cryptohub.domain.models;

public class Post {

    private Integer userId;
    private Integer postId;
    private String post;

    public Post(Integer userId, Integer postId,String post) {
        this.userId = userId;
        this.postId = postId;
        this.post = post;
    }

    public Integer getUserId() {
        return userId;
    }

    public void setUserId(Integer userId) {
        this.userId = userId;
    }

    public Integer getPostId() {
        return postId;
    }

    public void setPostId(Integer postId) {
        this.postId = postId;
    }

    public String getPost() {
        return post;
    }

    public void setPost(String post) {
        this.post = post;
    }
}

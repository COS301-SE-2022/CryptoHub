package com.api.cryptohub.domain.models;

public class Like {

    private Integer userId;
    private Integer postId;
    private Boolean isLiked;

    public Like(Integer userId, Integer postId, Boolean isLiked) {
        this.userId = userId;
        this.postId = postId;
        this.isLiked = isLiked;
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

    public Boolean getLiked() {
        return isLiked;
    }

    public void setLiked(Boolean liked) {
        isLiked = liked;
    }
}

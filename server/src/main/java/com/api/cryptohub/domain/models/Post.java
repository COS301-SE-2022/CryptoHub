package com.api.cryptohub.domain.models;

import com.fasterxml.jackson.annotation.JsonIgnore;

import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.SequenceGenerator;
import javax.persistence.*;

import static javax.persistence.GenerationType.SEQUENCE;

@Entity(name = "Post")
@Table(
        name = "posts"
)
public class Post {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(
            name = "postid",
            updatable = false,
            columnDefinition = "serial"
    )
    private Integer postId;
    @Column(
            name = "post",
            nullable = false,
            columnDefinition = "TEXT"
    )
    private String post;

    @JsonIgnore
    @ManyToOne
    @JoinColumn(name = "user_id")
    private User user;

    @Transient
    private Integer userid = null;

    public Post(Integer postId, String post) {
        this.postId = postId;
        this.post = post;
    }

    public Post(String post) {
        this.post = post;
    }

    public Post() {

    }

    public User getUser() {
        return user;
    }

    public void setUser(User user) {
        this.user = user;
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

    public Integer getUserid() {
        if (userid != null)
            return userid;

        return user.getUserId();
    }

    public void setUserid(Integer userid) {
        this.userid = userid;
    }
}

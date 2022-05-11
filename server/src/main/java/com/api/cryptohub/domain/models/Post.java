package com.api.cryptohub.domain.models;

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
@SequenceGenerator(
        name = "posts_sequence",
        sequenceName = "posts_sequence",
        allocationSize = 1
)

@GeneratedValue(
        strategy = SEQUENCE,
        generator = "posts_sequence"
)

@Column(
        name = "postid",
        updatable = false
)
    private Integer postId;
@Column(
        name = "post",
        nullable = false,
        columnDefinition = "TEXT"
)
    private String post;

@ManyToOne(
        cascade = CascadeType.ALL
)
@JoinColumn(name = "user_id")
private User user;

    public Post(Integer postId,String post) {
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
}

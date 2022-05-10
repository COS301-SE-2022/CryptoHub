package com.api.cryptohub.domain.models;

import javax.persistence.*;

import static javax.persistence.GenerationType.SEQUENCE;

@Entity(name = "User")
@Table(name="users")
public class User {

    @Id
    @SequenceGenerator(
            name =  "users_sequence",
            sequenceName  = "users_sequence",
            allocationSize = 1

    )

    @GeneratedValue(
            strategy = SEQUENCE,
            generator = "users_sequence"

    )

    @Column(
            name = "userid",
            updatable = false
    )
    private Integer userId;
    @Column(
            name = "first_name",
            updatable = true
    )
    private String fistName;
    @Column(
            name = "last_name",
            updatable = true
    )
    private String lastName;
    @Column(
            name = "email",
            updatable = true
    )
    private String email;
    @Column(
            name = "password",
            updatable = true
    )
    private String password;
    @Column(
            name = "user_name",
            updatable = true
    )
    private String userName;

    public User() {
    }

    public User(
            Integer userId,
            String fistName,
            String lastName,
            String email,
            String password,
            String userName) {
        this.userId = userId;
        this.fistName = fistName;
        this.lastName = lastName;
        this.email = email;
        this.password = password;
        this.userName = userName;
    }

    public Integer getUserId() {
        return userId;
    }

    public void setUserId(Integer userId) {
        this.userId = userId;
    }

    public String getFistName() {
        return fistName;
    }

    public void setFistName(String fistName) {
        this.fistName = fistName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }
}

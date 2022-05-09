package com.api.cryptohub.mocks;

import com.api.cryptohub.domain.models.User;

import java.util.*;

public class UserMock {

    public static ArrayList<User> userMock = new ArrayList<>(
            List.of(
                    new User(1,"bruce","wayne","batman@gmail.com",
                            "P@55w0rd","batman"),
                    new User(2,"peter","parker","spiderman@gmail.com",
                            "123four","spiderman"),
                    new User(3,"clark","kent","superman@gmail.com",
                            "mypassword","superman"),
                    new User(4,"tony","stark","ironman@gmail.com",
                            "avenger","ironman")
                    )
    );
}

package com.api.cryptohub.mocks;

import com.api.cryptohub.domain.models.Like;

import java.util.ArrayList;
import java.util.List;

public class LikeMock {

    public static List<Like> likeMock = new ArrayList<>(
        List.of(
                new Like(1,2,true),
                new Like(1,3,true),
                new Like(1,4,true),
                new Like(2,1,true),
                new Like(2,3,false),
                new Like(3,2,true),
                new Like(4,2,true)
        )
    );



}

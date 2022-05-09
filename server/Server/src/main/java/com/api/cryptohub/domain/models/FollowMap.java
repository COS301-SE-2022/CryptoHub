package com.api.cryptohub.domain.models;

import javax.persistence.criteria.CriteriaBuilder;

public class FollowMap {

    private Integer targetId;
    private Integer followerId;

    public FollowMap(Integer targetId, Integer followerId) {
        this.targetId = targetId;
        this.followerId = followerId;
    }
}

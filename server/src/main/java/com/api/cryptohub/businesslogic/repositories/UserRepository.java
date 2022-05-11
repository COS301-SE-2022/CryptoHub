package com.api.cryptohub.businesslogic.repositories;

import com.api.cryptohub.domain.models.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface UserRepository extends JpaRepository<User, Integer> {

    @Query("select u from User u where u.following in :userid")
    List<User> getUserFollowers(Integer userid);
}

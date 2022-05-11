package com.api.cryptohub.businesslogic.repositories;

import com.api.cryptohub.domain.models.Post;
import com.api.cryptohub.domain.models.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface PostRepository extends JpaRepository<Post,Integer> {

    @Query("select p from Post p where p.user.userId = :userid")
    List<Post> findPostsByUserId(Integer userid);
}

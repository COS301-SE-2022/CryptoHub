package com.api.cryptohub.mocks;

import com.api.cryptohub.businesslogic.repositories.PostRepository;
import com.api.cryptohub.businesslogic.repositories.UserRepository;
import com.api.cryptohub.domain.models.Post;
import com.api.cryptohub.domain.models.User;
import org.springframework.boot.CommandLineRunner;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import java.util.List;

@Configuration
public class PostConfig {

    @Bean
    CommandLineRunner commandLineRunner(
            PostRepository postRepository
    )
    {
        return args -> {

            User one = new User();
            one.setUserId(1);

            User two = new User();
            one.setUserId(2);

            User three = new User();
            one.setUserId(3);


            Post postone = new Post("hello");
            postone.setUser(one);

            Post posttwo = new Post("hello it's me");
            posttwo.setUser(one);

            Post postthree = new Post("whatsup");
            postthree.setUser(one);

            Post postfour = new Post("how are you");
            postfour.setUser(one);

            Post postfive = new Post("this coin is cool");
            postfive.setUser(one);


            postRepository.saveAll(
                    List.of(
                    postone, posttwo, postthree, postfour, postfive
                    )
            );
        };
    }
}

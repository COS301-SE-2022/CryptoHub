/*
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




                    new User("tony","stark","ironman@gmail.com",
                            "avenger","ironman"),
                    new User("stephen","strange","drstrange@gmail.com",
                            "doctor","drstrange"),
                    new User("loki","odinson","mischief@gmail.com",
                            "evil","mischief")
                    )
            User one = new User("bruce","wayne","batman@gmail.com",
                    "P@55w0rd","batman")
            one.setUserId(1);

            User two = new User("clark","kent","superman@gmail.com",
                    "mypassword","superman")
            one.setUserId(2);

            User three = new User("peter","parker","spiderman@gmail.com",
                    "123four","spiderman"),
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
*/

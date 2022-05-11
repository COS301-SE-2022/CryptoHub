package com.api.cryptohub.mocks;

import com.api.cryptohub.businesslogic.repositories.UserRepository;
import com.api.cryptohub.domain.models.User;
import org.springframework.boot.CommandLineRunner;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import java.util.List;

@Configuration
public class UserConfig {

    @Bean
    CommandLineRunner commandLineRunner(
            UserRepository userRepository
    )
    {
        return args -> {

            userRepository.saveAll(
                    List.of(
            new User("bruce","wayne","batman@gmail.com",
                                    "P@55w0rd","batman"),
            new User("peter","parker","spiderman@gmail.com",
                    "123four","spiderman"),
            new User("clark","kent","superman@gmail.com",
                    "mypassword","superman"),
            new User("tony","stark","ironman@gmail.com",
                    "avenger","ironman"),
            new User("stephen","strange","drstrange@gmail.com",
                    "doctor","drstrange"),
            new User("loki","odinson","mischief@gmail.com",
                    "evil","mischief")
                    )
            );
        };
    }
}

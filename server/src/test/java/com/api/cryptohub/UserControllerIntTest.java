package com.api.cryptohub;


import com.api.cryptohub.businesslogic.repositories.UserRepository;
import com.api.cryptohub.domain.models.User;
import org.hamcrest.CoreMatchers;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.AutoConfigureMockMvc;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.ResultActions;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;
import org.springframework.test.web.servlet.result.MockMvcResultMatchers;


import java.util.List;

@SpringBootTest
@AutoConfigureMockMvc
class UserControllerIntTest {

    @Autowired
    private UserRepository userRepository;

    @Autowired
    private MockMvc mockMvc;


    @Test
    public void givenUser_whenGetAllUsers_thenListOfUsers() throws Exception
    {

        List<User> users = List.of(new User("Casparus", "Bresler", "theMan@gmail.com", "123", "TheGhost"));
        userRepository.saveAll(users);

        ResultActions response = mockMvc.perform(MockMvcRequestBuilders.get("/api/user/getallusers"));
        response.andExpect(MockMvcResultMatchers.status().isOk());
        response.andExpect(MockMvcResultMatchers.jsonPath("$.size()", CoreMatchers.is(users.size() + 6)));
    }



}

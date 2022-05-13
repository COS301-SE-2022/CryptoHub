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

        List<User> users = List.of(new User("John", "Smith", "Adress@gmail.com", "123", "user"));
        userRepository.saveAll(users);

        ResultActions response = mockMvc.perform(MockMvcRequestBuilders.get("/api/user/getallusers"));
        response.andExpect(MockMvcResultMatchers.status().isOk());
        response.andExpect(MockMvcResultMatchers.jsonPath("$.size()", CoreMatchers.is(userRepository.findAll().size())));
    }

    @Test
    public void givenUserId_whenGetUserById_thenUserOfId() throws Exception
    {
        User user = new User("John", "Smith", "Adress2@gmail.com", "123", "user2");

        userRepository.save(user);
        int num = userRepository.findAll().size();
        ResultActions response = mockMvc.perform(MockMvcRequestBuilders.get("/api/user/" + num));

        response.andExpect(MockMvcResultMatchers.status().isOk());
        response.andExpect(MockMvcResultMatchers.jsonPath("$.userName", CoreMatchers.is(user.getUserName())));
    }

//    @Test
//    public void givenUserId_whenGetFollowing_thenUserOfFollowing() throws Exception
//    {
//        User user = userRepository.getById(6);
//
//        UserController.FollowDTO followDTO = new UserController.FollowDTO(1,6);
//
//        ObjectMapper mapper = new ObjectMapper();
//        mapper.configure(SerializationFeature.WRAP_ROOT_VALUE, false);
//        ObjectWriter ow = mapper.writer().withDefaultPrettyPrinter();
//        String requestJson=ow.writeValueAsString(followDTO);
//
//        ResultActions response = mockMvc.perform(MockMvcRequestBuilders.post("/api/user/follow").contentType(APPLICATION_JSON).content(requestJson));
//
//        response.andExpect(MockMvcResultMatchers.status().isOk());
//        response.andExpect(MockMvcResultMatchers.content().string("following " + user.getUserName()));
//    }

}

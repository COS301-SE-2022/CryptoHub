package com.api.cryptohub;

import com.api.cryptohub.api.controllers.AuthorizationController;
import com.api.cryptohub.businesslogic.repositories.UserRepository;
import com.api.cryptohub.domain.models.User;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.hamcrest.Matcher;
import org.hamcrest.Matchers;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.Mockito;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.boot.test.mock.mockito.MockBean;
import org.springframework.http.MediaType;
import org.springframework.test.context.junit.jupiter.SpringExtension;

import org.springframework.test.web.servlet.MockMvc;
import org.springframework.test.web.servlet.request.MockHttpServletRequestBuilder;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;

import java.lang.reflect.Field;

import static net.bytebuddy.matcher.ElementMatchers.is;
import static org.hamcrest.Matchers.notNullValue;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;

@ExtendWith(SpringExtension.class)
@WebMvcTest(AuthorizationController.class)
public class AuthorizationControllerIntTest {

    @Autowired
    MockMvc mockMvc;
    @Autowired
    ObjectMapper mapper;
    @MockBean
    private UserRepository userRepository;

//    @Before
//    public void setup()
//    {
//        MockitoAnnotations.openMocks(this);
//        ///this.mockMvc = MockMvcRequestBuilders.standalOneSetup()
//    }

    @Test
    void Register_UnregisteredUser_ReturnError() throws Exception
    {
        User record = new User();
        record.setEmail("");

        Mockito.when(userRepository.save(record)).thenReturn(record);

        MockHttpServletRequestBuilder mockRequest = MockMvcRequestBuilders.post("http://localhost:8081/api/authorization/register")
                .contentType(MediaType.APPLICATION_JSON)
                .accept(MediaType.APPLICATION_JSON)
                .content(this.mapper.writeValueAsString(record));

        mockMvc.perform(mockRequest)
                .andExpect(status().is4xxClientError())
                .andExpect(jsonPath("$",notNullValue()))
                .andExpect(jsonPath("$.userId", Matchers.is((Field) null)))
                .andExpect(jsonPath("$.response", Matchers.is("user already exists")));
        ;
    }

//    @Test
//    void authorize() throws Exception
//    {
//        User user = new User();
//
//        RequestBuilder request = MockMvcRequestBuilders.post().;
//        MvcResult result = mvc.perform(request).andReturn();
//        assertEquals(user, result.getResponse().getClass());
//    }
}

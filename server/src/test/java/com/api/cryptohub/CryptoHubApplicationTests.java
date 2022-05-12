package com.api.cryptohub;

import com.api.cryptohub.api.controllers.AuthorizationController;
import org.junit.jupiter.api.Test;
import org.springframework.boot.test.context.SpringBootTest;

import static org.junit.jupiter.api.Assertions.assertEquals;

@SpringBootTest
class CryptoHubApplicationTests {

    @Test
    void contextLoads() {
        AuthorizationController.Response controller = new AuthorizationController.Response("Test",true,1,"");
        String response = controller.getResponse();
        assertEquals("Test", response);
    }



}

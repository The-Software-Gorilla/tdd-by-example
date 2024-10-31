package com.thesoftwaregorilla.helper;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.junit.jupiter.api.Disabled;
import org.junit.jupiter.api.Test;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;

public class JsonHelperTest {

    @Test
    @Disabled("Test needs work. We need a sample JSON file to test against.")
    void testParseResponseWithFile() throws IOException {
        String expectedResponse = "TestObject";
        File responseFile = Files.writeString(Files.createTempFile(null, null), expectedResponse).toFile();

        class TestClass {
            private String testString;

            public String getTestString() {
                return testString;
            }

            public void setTestString(String testString) {
                this.testString = testString;
            }
        }

        TestClass result = JsonHelper.parseResponse(responseFile, TestClass.class);
        assertEquals(expectedResponse, result.getTestString());
    }

    @Test
    void testParseResponseWithFileThrowsIOException() {
        File invalidFile = new File("/path/to/invalid/file");
        assertThrows(IOException.class, () -> JsonHelper.parseResponse(invalidFile, Object.class));
    }

    @Test
    @Disabled("Test needs work. We need a sample JSON string to test against.")
    void testParseResponseWithString() throws IOException {
        String expectedResponse = "TestStringResponse";
        String response = "{\"testString\":\"" + expectedResponse + "\"}";

        class TestClass {
            private String testString;

            public String getTestString() {
                return testString;
            }

            public void setTestString(String testString) {
                this.testString = testString;
            }
        }

        TestClass result = JsonHelper.parseResponse(response, TestClass.class);
        assertEquals(expectedResponse, result.getTestString());
    }

    @Test
    void testParseResponseWithStringThrowsJsonProcessingException() {
        String invalidJson = "invalidJson";
        assertThrows(IOException.class, () -> JsonHelper.parseResponse(invalidJson, Object.class));
    }
}
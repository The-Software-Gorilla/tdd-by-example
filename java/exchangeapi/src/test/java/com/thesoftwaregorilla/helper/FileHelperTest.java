package com.thesoftwaregorilla.helper;

import org.junit.jupiter.api.Test;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertThrows;

public class FileHelperTest {

    @Test
    public void testWriteStringToFile_Success() throws IOException {
        String testString = "Hello, world!";
        String testPath = "./test.txt";
        FileHelper.writeStringToFile(testString, testPath);

        Path path = Paths.get(testPath);
        String resultString = Files.readString(path);
        assertEquals(testString, resultString);

        // clean up
        Files.delete(path);
    }

    @Test
    public void testWriteStringToFile_IOException() {
        String testString = "Hello, world!";
        String invalidPath = "/invalid/path/test.txt";

        assertThrows(IOException.class, () -> FileHelper.writeStringToFile(testString, invalidPath));
    }
}
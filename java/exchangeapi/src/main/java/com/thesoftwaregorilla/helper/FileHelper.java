package com.thesoftwaregorilla.helper;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;

public class FileHelper {

    public static void writeStringToFile(String response, String filePath) throws IOException {
        Path path = Paths.get(filePath);
        Files.write(path, response.getBytes());
    }

}

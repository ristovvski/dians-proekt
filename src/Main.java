import java.io.*;
import java.util.List;
import com.google.gson.Gson;

class ObjectData {
    double lon;
    double lat;
    String name;
    String type;
    String region;
}

public class Main {
    public static void main(String[] args) {
        String inputFilePath = "input.json";
        String consoleOutputFilePath = "console_output.txt";
        String jsonOutputFilePath = "output_with_regions.json";

        System.out.println("Absolute Path: " + new File(inputFilePath).getAbsolutePath());

        // Parse the JSON data from the file into a list of objects
        List<ObjectData> objects = parseJsonFile(inputFilePath);

        // Add regions to each object
        for (ObjectData obj : objects) {
            obj.region = getRegion(obj.lat, obj.lon);
        }

        // Write the updated list of objects to a JSON file
        writeToJsonFile(objects, jsonOutputFilePath);
    }

    private static void writeToJsonFile(List<ObjectData> objects, String outputFilePath) {
        try (Writer writer = new FileWriter(outputFilePath)) {
            Gson gson = new Gson();
            gson.toJson(objects, writer);
        } catch (IOException e) {
            e.printStackTrace();
            // Handle the exception as needed
        }
    }

    private static List<ObjectData> parseJsonFile(String filePath) {
        try (InputStream inputStream = Main.class.getClassLoader().getResourceAsStream(filePath)) {
            if (inputStream != null) {
                InputStreamReader reader = new InputStreamReader(inputStream);
                Gson gson = new Gson();
                return List.of(gson.fromJson(reader, ObjectData[].class));
            } else {
                System.out.println("File not found: " + filePath);
                return List.of(); // Return an empty list if the file is not found
            }
        } catch (IOException e) {
            e.printStackTrace();
            // Handle the exception as needed
            return List.of(); // Return an empty list in case of an error
        }
    }

    private static String getRegion(double latitude, double longitude) {
        // Specify the latitude and longitude bounds for each region with expanded ranges
        if (isInsideBounds(latitude, longitude, 41.8, 42.2, 21.0, 22.5)) {
            return "Skopje Region";
        } else if (isInsideBounds(latitude, longitude, 41.5, 42.5, 20.5, 22.5)) {
            return "Polog Region";
        } else if (isInsideBounds(latitude, longitude, 41.2, 42.2, 20.5, 22.5)) {
            return "Vardar Region";
        } else if (isInsideBounds(latitude, longitude, 41.0, 42.2, 22.0, 23.0)) {
            return "Eastern Region";
        } else if (isInsideBounds(latitude, longitude, 41.8, 42.2, 21.5, 22.5)) {
            return "Northeastern Region";
        } else if (isInsideBounds(latitude, longitude, 40.8, 42.0, 22.0, 23.0)) {
            return "Southeastern Region";
        } else if (isInsideBounds(latitude, longitude, 40.5, 42.0, 20.5, 22.5)) {
            return "Southwestern Region";
        } else if (isInsideBounds(latitude, longitude, 41.0, 42.5, 21.0, 22.5)) {
            return "Pelagonia Region";
        }

        // Default to an unknown region if no match is found
        return "Unknown";
    }


    private static boolean isInsideBounds(double latitude, double longitude, double minLat, double maxLat, double minLon, double maxLon) {
        return latitude >= minLat && latitude <= maxLat && longitude >= minLon && longitude <= maxLon;
    }
}

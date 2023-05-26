    /// <summary>
    /// An example of a function that would be placed in an event class
    /// </summary>
    void ExampleWriter()
    {
        //Simply creates the dummy class containing arbitrary values
        dummy d = new dummy();

        ////Creates a new point with a preset measurement (database)
        Point p = new Point("dummy_metric");

        //At least one tag must be used, but you can use as many as you wish
        //Tags are used for easier querying in the Influx database
        p.addTag("AIname", d.name);
        p.addTag("host", "unity5.6");

        //At least one field must be used, but you can use as many as you wish
        //Fields are used to store values that you want to be stored in the Influx database
        p.addField("valueOne", d.values[0]);
        p.addField("valueTwo", d.values[1]);
        p.addField("age", d.age);
        p.addField("isFemale", d.isFemale);

        //Adding timestamps are optional, as the Influx database autogenerates(server time, which need to be noted) them if they are not given during transmit
        p.addTimestamp(System.DateTime.UtcNow);

        //Transmits all stored data to the Influx database.
        influx.writeMeasurment(GetComponent <MonoBehaviour>(), p);
    }

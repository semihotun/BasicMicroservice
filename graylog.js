graylog = db.getSiblingDB('graylog');
graylog.createUser(
    {
        user: "root",
        pwd: "semihO123",
        roles: [
            { role: "dbOwner", db: "graylog" }
        ]
    }
);
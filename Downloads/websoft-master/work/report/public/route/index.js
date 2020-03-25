/**
 * General routes.
 */
"use strict";

const express = require("express");
const router  = express.Router();

// Add a route for the path /
router.get("/", (req, res) => {
    res.send("Hello World");
});

// Add a route for the path /about
router.get("/about", (req, res) => {
    res.send("About something");
});

// Add a route for the path /lotto
router.get("/lotto", (req, res) => {
    res.send("Lotto numbers:");
});

module.exports = router;

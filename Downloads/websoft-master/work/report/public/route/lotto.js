"use strict";


var express = require("express");
var router  = express.Router();

router.get("/", (req, res) => {
    var min=1;
    var max=35;
    var random = Math.random() * (+max - +min) + + min;
    document.write("Random Number Generated : " + random );
});

module.exports = router;

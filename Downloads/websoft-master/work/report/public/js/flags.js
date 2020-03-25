(function () {
    'use strict';

    //var myContent = document.getElementById('content');
    var flagTarget = document.getElementById('flag');
    var flagLinkElf = document.getElementById('draw-elfenbenskusten');
    var flagLinkFrance = document.getElementById('draw-france');
    var flagLinkBelg = document.getElementById('draw-belg');

    //myContent.innerHTML = '<h3>This is a MEGA template!</h3>';

    function drawFlagElfenbenskusten()  {
        var flagElfenbenskusten = '<div class="flag elfenbenskusten"><div class="part1"></div>' +
        '<div class="part2"></div></div>';
        console.log("Drawing flag");
        flagTarget.innerHTML = flagElfenbenskusten;
    }

    function drawFlagFrance()  {
        var flagFrance = '<div class="flag france"><div class="part1"></div>' +
        '<div class="part2"></div></div>';
        console.log("Drawing flag");
        flagTarget.innerHTML = flagFrance;
    }

    function drawFlagBelg()  {
        var flagBelg = '<div class="flag belg"><div class="part1"></div>' +
        '<div class="part2"></div></div>';
        console.log("Drawing flag");
        flagTarget.innerHTML = flagBelg;
    }

    flagTarget.addEventListener("click", function (){
        flagTarget.classList.toggle("m-fadeOut")
        console.log("Hello")
    });

    flagLinkElf.addEventListener("click", function () {

        drawFlagElfenbenskusten();

    });

    flagLinkFrance.addEventListener("click", function () {

        drawFlagFrance();

    });

    flagLinkBelg.addEventListener("click", function () {

        drawFlagBelg();

    });


    console.log('Sandbox MEGA is ready!');
})();

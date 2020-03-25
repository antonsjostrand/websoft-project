function hideTheDuck() {
  var elem = document.getElementById("myDuck");
  elem.style.display = "none";
  var wait = 0;
  var timer = setInterval(function() {
    if (wait == 1) {
      clearInterval(timer);
      elem.style.display = "block";
      var w = window.innerWidth-elem.style.width;
      if(w<0) w=0;
      var h = window.innerHeight-elem.style.height;
      if(h<0) h=0;
      var left = Math.floor(w* Math.random());
      var top = Math.floor(h* Math.random());
      elem.style.top = top + "px";
      elem.style.left = left + "px";


    }
    wait++;
  }, 1000);
}

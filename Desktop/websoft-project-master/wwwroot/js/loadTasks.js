function loadTasks(val){
    "use strict";
    
    fetch("todo/list/" + val)
        .then((response) => {
            return response.json();
        })
        .then((myJson) => {

            var tasks = myJson;

            var monday = document.getElementById("monday");
            var tuesday = document.getElementById("tuesday");
            var wednesday = document.getElementById("wednesday");
            var thursday = document.getElementById("thursday");
            var friday = document.getElementById("friday");
            var saturday = document.getElementById("saturday");
            var sunday = document.getElementById("sunday");

            var innerHtmlMonday = "<p>Monday</p>";
            var innerHtmlTuesday = "<p>Tuesday</p>";
            var innerHtmlWednesday = "<p>Wednesday</p>";
            var innerHtmlThursday = "<p>Thursday</p>";
            var innerHtmlFriday = "<p>Friday</p>";
            var innerHtmlSaturday = "<p>Saturday</p>";
            var innerHtmlSunday = "<p>Sunday</p>";

            if(tasks.length > 0){

                tasks.forEach(element => {
                    
                    switch(element.weekDay){

                        case "Monday":
                            innerHtmlMonday += "<div class=dayDiv onclick=\"edit("+element.taskId+")\"><p> <u>Title:</u> " + element.title + "<br>" + " <u>Description:</u>" + element.description + "</p></div>";

                        break;
                        case "Tuesday":
                            innerHtmlTuesday += "<div class=dayDiv onclick=\"edit(" + element.taskId + ")\"><p> <u>Title:</u> " + element.title + "<br>" + " <u>Description:</u> " + element.description + "</p></div>";

                        break;
                        case "Wednesday":
                            innerHtmlWednesday += "<div class=dayDiv onclick=\"edit(" + element.taskId + ")\"><p> <u>Title:</u> " + element.title + "<br>" + " <u>Description:</u> " + element.description + "</p></div>"; 
                            
                        break;
                        case "Thursday":
                            innerHtmlThursday += "<div class=dayDiv onclick=\"edit(" + element.taskId + ")\"><p> <u>Title:</u> " + element.title + "<br>" + " <u>Description:</u> " + element.description + "</p></div>"; 
                            
                        break;
                        case "Friday":
                            innerHtmlFriday += "<div class=dayDiv onclick=\"edit(" + element.taskId + ")\"><p> <u>Title:</u> " + element.title + "<br>" + " <u>Description:</u> " + element.description + "</p></div>";
                            
                        break;
                        case "Saturday":
                            innerHtmlSaturday += "<div class=dayDiv onclick=\"edit(" + element.taskId + ")\"><p> <u>Title:</u> " + element.title + "<br>" + " <u>Description:</u> " + element.description + "</p></div>";
                            
                        break;
                        case "Sunday":
                            innerHtmlSunday += "<div class=dayDiv onclick=\"edit(" + element.taskId + ")\"><p> <u>Title:</u> " + element.title + "<br>" + " <u>Description:</u> " + element.description + "</p></div>";
                            
                        break;
                    }
                });          
            }

        monday.innerHTML = innerHtmlMonday;
        tuesday.innerHTML = innerHtmlTuesday;
        wednesday.innerHTML = innerHtmlWednesday;
        thursday.innerHTML = innerHtmlThursday;
        friday.innerHTML = innerHtmlFriday;
        saturday.innerHTML = innerHtmlSaturday;
        sunday.innerHTML = innerHtmlSunday;

        });

    


}
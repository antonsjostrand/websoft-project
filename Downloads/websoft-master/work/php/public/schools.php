<?php require __DIR__ . '/view/header.php'; ?>


<body>
<h1>Schools</h1>

<div id="content">
    <p>Choose city to fetch schools!</p>
</div>




<form name="form2">
    <select name="jumpmenu" onChange="jumpto(document.form2.jumpmenu.options[document.form2.jumpmenu.options.selectedIndex].value)">
    <option>Choose "kommun"...</option>
    <option value='https://cors-anywhere.herokuapp.com/https://api.scb.se/UF0109/v2/skolenhetsregister/sv/kommun/1282'>Landskrona</option>
    <option value='https://cors-anywhere.herokuapp.com/https://api.scb.se/UF0109/v2/skolenhetsregister/sv/kommun/1281',>Lund</option>
    <option value='https://cors-anywhere.herokuapp.com/https://api.scb.se/UF0109/v2/skolenhetsregister/sv/kommun/1290',>Kristianstad</option>
    </select>
    </form>





<div id="container"></div>
<script type="text/javascript" src="js/fetchSchools.js">jumpto(document.form2.jumpmenu.options[document.form2.jumpmenu.options.selectedIndex].value);</script>
<div id="duck" class="duck"></div>
<script type="text/javascript" src="js/duck.js"></script>


<?php require __DIR__ . '/view/footer.php'; ?>

</body>
</html>

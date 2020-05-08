$( document ).ready(function() {
	console.log('Starting SpeechRecognition library.');
	var speech = new Speech();

    speech.recognition.onstart = function() {
		//$('#capture').text("Stop");
		$('#capture').val("false");
		//$('#status').text("Listening...");
    	console.log('Listening started...');
    }

	speech.recognition.onend = function() {
		//$('#capture').text("Start");
		$('#capture').val("true");
		//$('#status').text("Idle");
    	console.log('Listening stopped.');
    }

    //$('#capture').click(function () {
	$(document).on("click", "#capture", function (event) {
	    
		if ($('#capture').val() == "true") {
			speech.startCapture();
		}
		else {
			speech.stopCapture();
		}
	});
});
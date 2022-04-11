
const baseURl = "https://localhost:5001/api/songs";
var songList = [];

function findSongs(){
    var url = "https://www.songsterr.com/a/ra/songs.json?pattern="
    let searchString = document.getElementById("searchSong").value;

    url += searchString;

    console.log(searchString)

    fetch(url).then(function(response) {
		console.log(response);
		return response.json();
	}).then(function(json) {
        console.log(json)
        let html = ``;

		json.forEach((song) => {
            console.log(song.title)
            html += `<div class="card col-md-4 bg-dark text-white">`;
			html += `<img src="./resources/images/music.jpeg" class="card-img" alt="...">`;
			html += `<div class="card-img-overlay">`;
			html += `<h5 class="card-title">`+song.title+`</h5>`;
            html += `</div>`;
            html += `</div>`;
		});
		
        if(html === ``){
            html = "No Songs found :("
        }
		document.getElementById("searchSongs").innerHTML = html;

	}).catch(function(error) {
		console.log(error);
	})
}

function handleOnLoad(){
    populateCards();
}

function populateCards(){
   fetch(baseURl).then(function(response){
       return response.json();
   }) .then(function(json) {
       songList = json;

        let html = '<div id="cards" class="container">';
        html += '<div class="row">'
        var count = 0;
       json.forEach((song) => {  
        console.log(song.songTitle)
        if (count % 3 != 0)
        {
            html += `<div class="card col-md-4 bg-dark text-white">`;
            html += `<img src="./resources/images/music.jpeg" class="card-img" alt="...">`;
            html += `<div class="card-img-overlay">`;
            //if song.favorited == 'y'
            html += `<h5 value =${song.songID} class="card-title">`+song.songTitle+`</h5>`;
            html += `</div>`;
            html += `</div>`;
        }

        if(count % 3 == 0)
        {
            html += "</div>"
            html += '<div class="row">'
            html += `<div class="card col-md-4 bg-dark text-white">`;
            html += `<img src="./resources/images/music.jpeg" class="card-img" alt="...">`;
            html += `<div class="card-img-overlay">`;
            html += `<h5 value =${song.songID} class="card-title">`+song.songTitle+`</h5>`;
            html += `</div>`;
            html += `</div>`;
        }
            count ++;
        })
            html += '</div>'
        document.getElementById("cards").innerHTML = html;
   
    }).catch(function(error){
        console.log(error);
    })
}

function postSong(){
    const SongTitle = document.getElementById("title").value;

    fetch(baseURl, {
        method: "POST",
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json'
        },
        body: JSON.stringify({
            SongTitle: SongTitle
        })
    })
    .then((response)=>{
        console.log(response);
        populateCards();
    })
}

const baseURl = "https://localhost:5001/api/songs";
var songList = [];

function findSongs(){
    var url = "https://www.songsterr.com/a/ra/songs.json?pattern="
    let searchString = document.getElementById("searchSong").value;

    url += searchString;

    

    fetch(url).then(function(response) {
		
		return response.json();
	}).then(function(json) {
        
        let html = ``;

		json.forEach((song) => {
            
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
        
        
        if (count % 3 != 0)
        {
            html += `<div class="card col-md-4 bg-dark text-white">`;
            html += `<img src="./resources/images/music.jpeg" class="card-img" alt="...">`;
            html += `<div class="card-img-overlay">`;
            html += `<h5 value =${song.songID} class="card-title">`+song.songTitle+`</h5>`;
            if (song.favorited == 'n')
            {
                html += `<button value = ${song.songID} id = 1 class="btn" onclick = "putSong(this)"><i class="fa-solid fa-star" ></i></button>`
            }
            if(song.favorited =='y')
            {
                html+= `<button id = ${song.songID} class="favoritebtn" onclick = "putSong(this)"><i class="fa-solid fa-star" ></i></button>`  
            }
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
            if (song.favorited == 'n')
            {
                html += `<button id = 1 class="btn" onclick = "putSong(this)"><i class="fa-solid fa-star" ></i></button>`
            }
            if(song.favorited =='y')
            {
                html+= `<button id =favoritestar  class="favoritebtn" onclick = "putSong(this)")><i class="fa-solid fa-star" ></i></button>`  
            }
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
    songList.forEach((song) =>{
        if(song.songTitle == SongTitle)
        {
            alert("Sorry, you cannot add songs with the same name. Hopefully, I'll get better with JavaScript/HTML soon. :(")
            SongTitle = "//"
        }
    })
    if(SongTitle != "//")
    {
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
            
            populateCards();
        })
    }
}

function putSong(element){
    var parent = element.previousElementSibling;

    var content = parent.innerHTML;
  
    songList.forEach((song) =>{
        if(song.songTitle == content)
        {
            foundSong = song;
        }
    })
    if(foundSong.favorited == "n")
    {
        foundSong.favorited ='y'
    }
    else if (foundSong.favorited == "y")
    {
        foundSong.favorited ='n'
    }
    const putSongURl = baseURl + "/" + foundSong.id
    
    fetch(putSongURl, {
        method: "PUT",
        headers: {
            "Accept": 'application/json',
            "Content-Type": 'application/json',

        },
        body: JSON.stringify(foundSong)
    })
    .then((response) =>{
    console.log(response);
    populateCards();
    });
}

function deleteSong(){
   const deletedSong = document.getElementById("deletedSong").value;
   var foundSong;
    songList.forEach((song) =>{
        if(song.songTitle.toLowerCase() == deletedSong.toLowerCase())
        {
            foundSong = song;
        }

    })

    if(foundSong == undefined)
    {
        alert("Song was not found.")
    }

    if(confirm("Song " + foundSong.songTitle + " will now be deleted.")  == true)
    {
        
        const putSongURl = baseURl + "/" + foundSong.id
    
        fetch(putSongURl, {
            method: "DELETE",
            headers: {
                "Accept": 'application/json',
                "Content-Type": 'application/json',
    
            },
            body: JSON.stringify(foundSong.id)
        })
        .then((response) =>{
        console.log(response);
        populateCards();
        });
        
    } else{
        alert("Song " + foundSong.songTitle + " was not deleted.")
        document.getElementById("deletedSong").value = "";
        document.getElementById("deletedSong").focus();
    }
    // alert("Song " + foundSong.songTitle + " will now be deleted.");
}


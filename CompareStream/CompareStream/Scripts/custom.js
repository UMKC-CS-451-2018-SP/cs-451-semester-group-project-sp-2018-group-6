$( "#addServiceForm" ).submit(function( event ) {
    event.preventDefault();

    var $form = $( this ),
        name = $form.find( "input[name='serviceName']" ).val(),
        price = $form.find( "input[name='servicePrice']" ).val(),
        url = $form.attr( "action" );

    var posting = $.post( url, { serviceName: name, servicePrice: price } );

    posting.done(function( data ) {
        var content = $( data ).filter( "#content" );
        $( "#result" ).empty().append( content );
    });
});

$( "#addShowForm" ).submit(function( event ) {
    event.preventDefault();

    var $form = $( this ),
        name = $form.find( "input[name='showName']" ).val(),
        url = $form.attr( "action" );

    var posting = $.post( url, { showName: name } );

    posting.done(function( data ) {
        var content = $( data ).filter( "#content" );
        $( "#result" ).empty().append( content );
    });
});

$( "#addNetworkForm" ).submit(function( event ) {
    event.preventDefault();

    var $form = $( this ),
        name = $form.find( "input[name='networkName']" ).val(),
        url = $form.attr( "action" );

    var posting = $.post( url, { networkName: name } );

    posting.done(function( data ) {
        var content = $( data ).filter( "#content" );
        $( "#result" ).empty().append( content );
    });
});

$( "#reportProblemForm" ).submit(function( event ) {
    event.preventDefault();

    var $form = $( this ),
        name = $form.find( "input[name='reportDescription']" ).val(),
        url = $form.attr( "action" );

    var posting = $.post( url, { reportDescription: name } );

    posting.done(function( data ) {
        var content = $( data ).filter( "#content" );
        $( "#result" ).empty().append( content );
    });
});

$( "#showNameSearch" ).autocomplete({
  source: [ "BoJack Horseman", "Black Mirror", "Twin Peaks", "Stranger Things", "Better Call Saul", "Breaking Bad", "It's Always Sunny in Philadelphia" ]
});


function addShowToList()
{
    document.getElementById("result").innerHTML += "<li class='list-group-item'>" + document.getElementById("showNameSearch").value + "</li>";
}

$( document ).ready(function() {
    if (Cookies.get('isAdmin') == "True") {
        $( "#admin-dropdown" ).show();
    }

    if (typeof Cookies.get('email') === 'undefined') {
        $( "#anonymous-login" ).show();
    }
});

function viewAccount(user_email, user_id)
{
    $( "#userInspector" ).empty().append(user_email + " has the user ID: " + user_id)
    .append("<br />Favorite TV Shows:<br />");
}

function editNetworkShow(network_id, show_id, containsShow)
{
    $.ajax({
        url: '/Home/EditNetworkShow?networkID=' + network_id + '&showID=' + show_id + '&containsShow=' + containsShow,
        dataType: 'html',
        type: 'get',
        cache: false,
        success: function(data) {
            $( "#result" ).empty().append(data);
        }
    });
}

function editShow(show_id)
{
    $( "#tv-show-" + show_id ).append('<div>Networks:<br />');
    $.ajax({
        url: '/Home/PullNetworks?showID=' + show_id,
        dataType: 'json',
        type: 'get',
        cache: false,
        success: function(data) {
            $(data.networks).each(function(index, value) {
            var isChecked = "";
            if (value.ContainsShow == true) isChecked = "checked";
            $( "#tv-show-" + show_id ).append('<input type="checkbox" class="form-check-input" ' + isChecked + ' onclick="editNetworkShow(' + value.ID + ', ' + show_id + ', ' + value.ContainsShow + ');" /> ' + value.Name + ' ');
            });
        }
    });
    $( "#tv-show-" + show_id ).append('</div>');
}

$( "#accountSearchForm" ).submit(function( event ) {
    event.preventDefault();
    var $form = $( this );
    searchEmail = $form.find( "input[name='accountEmail']").val();

    $.ajax({
        url: '/Home/SearchUsers?email=' + searchEmail,
        dataType: 'json',
        type: 'get',
        cache: false,
        success: function(data) {
            $( "#result" ).empty();
            $(data.users).each(function(index, value) {
            $( "#result" ).append('<li class="list-group-item">Email: ' + value.Email + ' [<a href="#" onclick="viewAccount(\'' + value.Email + '\', ' + value.ID + ');">Inspect</a>]</li>');
            });
        }
    });
});

$( "#browseReportsForm" ).submit(function( event ) {
    event.preventDefault();
    var $form = $( this );
    offset = $form.find( "input[name='reportOffset']").val();

    $.ajax({
        url: '/Home/BrowseReports?offset=' + offset,
        dataType: 'json',
        type: 'get',
        cache: false,
        success: function(data) {
            $( "#result" ).empty();
            $(data.reports).each(function(index, value) {
            $( "#result" ).append('<li class="list-group-item">Report: ' + value.Description +'</li>');
            });
        }
    });
});

$( document ).ready(function() {
    $.ajax({
        url: '/Home/BrowseShows',
        dataType: 'json',
        type: 'get',
        cache: false,
        success: function(data) {
            $(data.shows).each(function(index, value) {
            $( "#showsList" ).append('<li class="list-group-item" id="tv-show-' + value.ID + '">' + value.Name +' [<a href="#" onclick="editShow(' + value.ID + ');return false;">Edit</a>]</li>');
            });
        }
    });
});
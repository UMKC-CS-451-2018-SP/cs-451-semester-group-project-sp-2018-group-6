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
            $( "#result" ).append('<li class="list-group-item">User: ' + value.UID + ' with report: ' + value.Description +'</li>');
            });
        }
    });
});
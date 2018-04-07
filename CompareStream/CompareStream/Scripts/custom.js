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
            $( "#result" ).append('<li class="list-group-item">Email: ' + value.Email + ' with user ID: ' + value.ID + ' [<a href="#' + value.ID + '">Inspect</a>]</li>');
            });
        }
    });
});
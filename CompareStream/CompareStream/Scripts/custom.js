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
    document.getElementById("result").innerHTML += document.getElementById("showNameSearch").value + "<br />";
}
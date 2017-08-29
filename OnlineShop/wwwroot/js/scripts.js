var clickedAmount = 1;

$(document).ready(function(){
  $("#another_address").click(function(){
    clickedAmount++;
    $(".add-address").append('<label for ="author">Type of Address</label>'+
    '<div class="form-group">'+
      '<input type="text" class="form-control" name="buyer-address-type' + clickedAmount + '">'+
    '</div>'+ '<label for ="author">Street</label>' +
    '<div class="form-group">' +
      '<input type="text" class="form-control" name="buyer-address-street' + clickedAmount + '">' +
    '</div>' +
    '<label for ="author">City</label>' +
    '<div class="form-group">' +
      '<input type="text" class="form-control" name="buyer-address-city' + clickedAmount + '">' +
    '</div>' +
    '<label for ="author">State</label>' +
    '<div class="form-group">' +
      '<input type="text" class="form-control" name="buyer-address-state' + clickedAmount + '">' +
    '</div>' +
    '<label for ="author">Country</label>' +
    '<div class="form-group">' +
      '<input type="text" class="form-control" name="buyer-address-country' + clickedAmount + '">' +
    '</div>' +
    '<label for ="author">Zip</label>' +
    '<div class="form-group">' +
      '<input type="text" class="form-control" name="buyer-address-zip' + clickedAmount + '">' +
    '</div>')
  });
  $("#shipping_address").click(function(){
    clickedAmount++;
    $("#add_new_shipping_address").append('<label for ="author">Type of Address</label>'+
    '<div class="form-group">'+
      '<input type="text" class="form-control" name="buyer-address-type' + clickedAmount + '">'+
    '</div>'+ '<label for ="author">Street</label>' +
    '<div class="form-group">' +
      '<input type="text" class="form-control" name="buyer-address-street' + clickedAmount + '">' +
    '</div>' +
    '<label for ="author">City</label>' +
    '<div class="form-group">' +
      '<input type="text" class="form-control" name="buyer-address-city' + clickedAmount + '">' +
    '</div>' +
    '<label for ="author">State</label>' +
    '<div class="form-group">' +
      '<input type="text" class="form-control" name="buyer-address-state' + clickedAmount + '">' +
    '</div>' +
    '<label for ="author">Country</label>' +
    '<div class="form-group">' +
      '<input type="text" class="form-control" name="buyer-address-country' + clickedAmount + '">' +
    '</div>' +
    '<label for ="author">Zip</label>' +
    '<div class="form-group">' +
      '<input type="text" class="form-control" name="buyer-address-zip' + clickedAmount + '">' +
    '</div>')
  });
});

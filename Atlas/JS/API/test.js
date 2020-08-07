
var obj = [
          {
              "price": 12.99,
              "title": "Sword of Honour",
              "category": "fiction",
              "author": "Evelyn Waugh"
          },
           {
               "price": 8.99,
               "title": "Moby Dick",
               "category": "fiction",
               "author": "Herman Melville",
               "isbn": "0-553-21311-3"
           },
           {
               "price": 8.95,
               "title": "Sayings of the Century",
               "category": "reference",
               "author": "Nigel Rees"
           },
           {
               "price": 22.99,
               "title": "The Lord of the Rings",
               "category": "fiction",
               "author": "J. R. R. Tolkien",
               "isbn": "0-395-19395-8"
           },
           {
               "price": 19.95,
               "brand": "Cannondale",
               "color": "red"
           }
]

function test() {

    JSON.search.trace = true;

    found = JSON.search(obj, '//*[price=22.99]');

    obj = $.grep(obj, function (e) { return e.price != 22.99 });

    alert('price:' + found[0].price);
    alert('title:' + found[0].title);
    alert('category:' + found[0].category);
    alert('author:' + found[0].author);
    found[0].title = '123';
    alert('title:' + found[0].title);
    //console.log(found[0]);
    //console.log(obj.store.book[0]);    
}
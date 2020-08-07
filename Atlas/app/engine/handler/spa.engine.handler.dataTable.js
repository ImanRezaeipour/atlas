define(["jquery", "ko", "spa.engine.infrastructure.cssLoader"], function (e, n, a) {
    a.load("/DesktopModules/Atlas/app/engine/infrastructure/dataTable/spa.engine.infrastructure.dataTable.css"),
   n.bindingHandlers.dataTable = {
       init: function (a, r, b, vm) {
           var t = n.unwrap(r());
           var ab = n.unwrap(b());

           if (ab.hasOwnProperty("dataTableControl") && ab.dataTableControl) {
               t.option.drawCallback = function (s) {
                   e("#" + s.sTableId + ' .dataTables_filter input').attr('placeholder', 'Search...');
                   e("#" + s.sTableId + ' .panel-ctrls').append(e('.dataTables_filter').addClass("pull-right")).find("label").addClass("panel-ctrls-center");
                   e("#" + s.sTableId + ' .panel-ctrls').append("<i class='separator'></i>");
                   e("#" + s.sTableId + ' .panel-ctrls').append(e('.dataTables_length').addClass("pull-left")).find("label").addClass("panel-ctrls-center");
                   e("#" + s.sTableId).parent().find('.dataTables_paginate>ul.pagination').addClass("pull-right m-n");
                   e("#" + s.sTableId).parent().parent().parent().find('.panel-footer').append(e("#" + s.sTableId).parent().find('.dataTables_paginate'));
               };
           }
           t.element = a,
           t.current = e(a).DataTable(t.option),
           t.current.on("draw", t.draw),
           t.chagePageLength = function (n) {
               t.current.destroy(),
               e(a).empty(),
               t.option.pageLength = n,
               t.current = e(a).DataTable(t.option),
               t.current.on("draw", t.draw)
           }

           e(t.element).on('click', 'td.actions button', function () {
               var index = e(this).attr("data-index");
               var parent = e(this).closest('tr');
               var rowIndex = t.current.row(parent).index();
               var rowdata = t.current.row(parent).data();
               t.actionButtons.buttons[index].action(rowIndex, rowdata)
           });
            
           if (t.selected) {
               e(t.element).find('tbody').on('click', 'tr', function () {
                   if (e(this).hasClass('selected')) {

                   }
                   else {
                       e(t.element).find('tr.selected').removeClass('selected');
                       $(this).addClass('selected');
                   }
               });
               e(t.element).on('click', 'tr', function () {
                   var parent = e(this);
                   var rowIndex = t.current.row(parent).index();
                   var rowdata = t.current.row(parent).data();
                   t.select(rowIndex);
                   t.selectData(rowdata);
               });
           }
           if (t.keybord) {
               require(["keyboardJS"], function (keyboardJS) {
                   function selectFirst() { e(t.element).find('tbody tr:first').trigger('click'); }
                   function selectLast() { e(t.element).find('tbody tr:last').trigger('click'); }
                   function selectUp() { e(t.element).find('tr.selected').prev().trigger('click'); }
                   function selectDown() { e(t.element).find('tr.selected').next().trigger('click'); }
                   function selectLeft() { e(t.element).next().find('.active').next().trigger('click'); }
                   function selectRight() { e(t.element).next().find('.active').prev().trigger('click'); }

                   keyboardJS.bind('enter', function (r) {
                       if (!e('body').hasClass("modal-open"))
                           e(t.element).find('tr.selected').find('td.actions button').trigger('click');
                   });
                   keyboardJS.bind('down', function (r) {
                       if (!e('body').hasClass("modal-open")) {
                           if (!e(t.element).find('tr.selected').length) selectFirst();
                           else selectDown();
                       }
                   });
                   keyboardJS.bind('up', function (r) {
                       if (!e('body').hasClass("modal-open")) {
                           if (!e(t.element).find('tr.selected').length) selectLast();
                           else selectUp();
                       }
                   });
                   keyboardJS.bind('left', function (r) {
                       if (!e('body').hasClass("modal-open"))
                           selectLeft();
                   });
                   keyboardJS.bind('right', function (r) {
                       if (!e('body').hasClass("modal-open"))
                           selectRight();
                   });
               });
           }
       }
   }
});
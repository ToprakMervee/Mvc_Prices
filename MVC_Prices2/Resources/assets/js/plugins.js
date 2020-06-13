
+function(){ 'use strict';
  
  var toggle = '[data-toggle="class"]';

  var ToggleClass = function() {};

  ToggleClass.prototype.toggle = function(e) {
    var $this = $(this);

    if ($this.is('.disabled, :disabled')) return;

    var target = $this.attr('data-target');
    var className = $this.attr('data-class');
    var isActive = $(target).hasClass(className);

    if (!isActive) {
      $(target).addClass(className);
      $this.attr('data-active', true);
    } else{
      $(target).removeClass(className);
      $this.attr('data-active', false);
    }

    if ($this.attr('self-toggle')) {
      var className = $this.attr('self-toggle');
      $this.toggleClass(className);
    }
  }

  $(document).on('click.app.toggleclass', toggle, ToggleClass.prototype.toggle);
}(jQuery);

var loader = loader || {};

+function($, $document, loader) { "use strict";
  var loaded = [],
  promise = false,
  deferred = $.Deferred();

  loader.load = function(srcs) {
    srcs = srcs || [];
    srcs = $.isArray(srcs) ? srcs : srcs.split(/\s+/);
    if (!promise) {
      promise = deferred.promise();
    }


    deferred.resolve();
    return promise;
  }

  // load javascript files

  

}(jQuery, document, loader);

+function($, LIBS) { 'use strict';
  $.fn.plugins = function(){

    var lists = this;

    lists.each(function() {
      var self = $(this);
      var options = {};
      var options = eval('[' + self.attr('data-options') + ']');
      if ($.isPlainObject(options[0])) {
        options[0] = $.extend({}, options[0]);
      }
      if(self.data('plugin') != '') {
        loader.load(LIBS[self.data('plugin')]).then( function(){
          // self refers to jQuery object
          self[self.attr('data-plugin')].apply(self, options);
        })
      }
    })

    return lists;
  }

}(jQuery, LIBS);
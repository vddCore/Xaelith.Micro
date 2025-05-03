window.xaelith = window.xaelith || { };
window.xaelith.admin = window.xaelith.admin || { 
    initSelect: (selector, enableSearch, allowClear, comboBoxPlaceholder, loaderSelector) => {
        let element = $(selector);
        
        if (!element.length)
            return;
        
        if (element?.hasClass("attached"))
            return;
        
        new Selectr(selector, {
            searchable: enableSearch,
            clearable: allowClear,
            allowDeselect: allowClear,
            placeholder: comboBoxPlaceholder
        });
        
        
        $(loaderSelector)?.hide();
        element.addClass("attached");
    }
};
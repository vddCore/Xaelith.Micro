window.xaelith = window.xaelith || { };
window.xaelith.admin = window.xaelith.admin || { 
    initSelect: (selector, enableSearch, allowClear, placeholder) => {
        new Selectr(selector, {
            searchable: enableSearch,
            clearable: allowClear,
            allowDeselect: allowClear,
            placeholder: placeholder
        });
    }
};
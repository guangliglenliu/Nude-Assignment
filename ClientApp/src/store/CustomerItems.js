import * as ActionType from "../constants/actionTypeConstants";

const initialState = { categories: [], customerItems: [], newItem: { name: '', value: 1, categoryId: 1 }, isLoading: false };

export const actionCreators = {
    getCustomerItems: customerId => async (dispatch) => {
        var baseURL = 'api/CustomerItem/';

        const resp_Category = await fetch(`${baseURL}Categories`);
        const cgs = await resp_Category.json();
        dispatch({ type: ActionType.RECEIVE_CATEGORY, cgs });

        const response = await fetch(`${baseURL}Get?customerId=${customerId}`);
        const items = await response.json();
        dispatch({ type: ActionType.RECEIVE_CUSTOMER_ITEMS, customerId, items });
    },

    addCustomerItem: data => async () => {
        await fetch('api/CustomerItem/Add', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        }).then(r => {
            if (r.status === 204) { alert("Duplicated or error"); }
            else {
                r.json().then(data => {
                    alert(`Added, item id is ${data}`);
                })
            }
        })

    },

    removeCustomerItem: itemId => async () => {
        await fetch(`api/CustomerItem/Remove?itemId=${itemId}`).then(r => r.json().then(data => {
            if (data === 0) { alert("Removed") }
            else if (data === 1) { alert("Not found") }
            else alert("error.")
        }));
    },

    setNewItemName: newItemName => ({ type: ActionType.SET_NEW_ITEM_NAME, newItemName }),

    setNewItemValue: newItemValue => ({ type: ActionType.SET_NEW_ITEM_VALUE, newItemValue }),

    setNewItemCategory: newItemCategoryId => ({ type: ActionType.SET_NEW_ITEM_CATEGORY, newItemCategoryId })
};

export const reducer = (state, action) => {
    state = state || initialState;
    if (action.type === ActionType.REQUEST_CATEGORY) {
        return {
            ...state,
            isLoading: true
        };
    }

    if (action.type === ActionType.RECEIVE_CATEGORY) {
        return {
            ...state,
            categories: action.cgs,
            isLoading: false
        };
    }

    if (action.type === ActionType.REQUEST_CUSTOMER_ITEMS) {
        return {
            ...state,
            customerId: action.customerId,
            isLoading: true
        };
    }

    if (action.type === ActionType.RECEIVE_CUSTOMER_ITEMS) {
        return {
            ...state,
            customerId: action.customerId,
            customerItems: action.items,
            isLoading: false
        };
    }

    if (action.type === ActionType.SET_NEW_ITEM_NAME) {
        return {
            ...state,
            newItem: { ...state.newItem, name: action.newItemName }
        };
    }

    if (action.type === ActionType.SET_NEW_ITEM_VALUE) {
        return {
            ...state,
            newItem: { ...state.newItem, value: action.newItemValue }
        };
    }
    if (action.type === ActionType.SET_NEW_ITEM_CATEGORY) {
        return {
            ...state,
            newItem: { ...state.newItem, categoryId: action.newItemCategoryId }
        };
    }

    return state;
};

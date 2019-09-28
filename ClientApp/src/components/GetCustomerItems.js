import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { actionCreators } from '../store/CustomerItems';

class GetCustomerItems extends Component {
    constructor() {
        super();

        this.addClick = this.addClick.bind(this);
        this.newItemNameChanged = this.newItemNameChanged.bind(this);
        this.newItemValueChanged = this.newItemValueChanged.bind(this);
        this.newItemCategoryChanged = this.newItemCategoryChanged.bind(this);
        this.removeItem = this.removeItem.bind(this);
    }

    componentDidMount() {
        // This method is called when the component is first added to the document
        this.getData();
    }

    getData() {
        this.props.getCustomerItems(1); // Hard code for demo, should be get from customer login
    }

    removeItem(e) {
        this.props.removeCustomerItem(e.target.id);
        alert("The selected item has been removed from the customer item list.");
        this.getData();
    }

    addClick() {
        this.props.addCustomerItem(this.props.newItem);
        alert("The selected item has been added from the customer item list.");
        this.getData();
    }

    newItemNameChanged(ev) {
        this.props.setNewItemName(ev.target.value);
    }

    newItemValueChanged(ev) {
        this.props.setNewItemValue(ev.target.value);
    }

    newItemCategoryChanged(ev) {
        this.props.setNewItemCategory(ev.target.value);
    }

  render() {
      return (
          <div>
            <h2>Cusotmer items</h2><br/>

              <div className="form-group col-10">
                  {this.props.customerItems.map((item, index) =>
                      <div key={index}>
                          <div id={item.categoryId} className="row form-group">
                              <div name={item.categoryId} className="col-5 font-weight-bold">{item.categoryName}</div>
                              <div name={item.categoryName} className="col-5 font-weight-bold">${item.categoryValue}</div>
                              <div className="col-2"></div>
                          </div>

                          <div>
                              {
                                  item.coverageItems.map(citem =>
                                      <div id={citem.itemId} key={citem.itemId} className="row form-group">
                                          <div className="col-5">
                                              <div className="col-1"></div>
                                              <div name={citem.name} className="col-11">{citem.name}</div>
                                          </div>
                                          <div name={citem.value} className="col-5">${citem.value}</div>
                                          <div className="col-1">
                                              <button id={citem.itemId} onClick={this.removeItem}><span><i id={citem.itemId} className="fa fa-trash-o" aria-hidden="true"></i></span>
                                              </button>
                                          </div>
                                      </div>
                                  )
                              }
                          </div>
                      </div>
                  )}
                  <div className="row">
                      <div className="col-5 font-weight-bold">TOTAL</div>
                      <div className="col-5 font-weight-bold">${this.props.customerItems.reduce((total, item) => total + item.categoryValue, 0)}</div>
                  </div>
              </div>

              <div className="row form-group">
                <input type="input" id="itemname" placeholder="Item Name" onChange={this.newItemNameChanged} className="col-3" />
                <input type="number" className="col-2" onChange={this.newItemValueChanged} min='1' defaultValue={this.props.newItem.value} />

                <select id="category" className="form-control col-3" onChange={this.newItemCategoryChanged}>
                    {this.props.categories.map(option =>
                        <option key={option.categoryId} value={option.categoryId} defaultValue={this.props.newItem.categoryId}>
                            {option.name}
                        </option>
                    )}
                </select>
                <button className="btn btn-primary col-1" onClick={this.addClick}>Add</button>
              </div>
              {this.props.isLoading ? <span>Loading...</span> : []}
      </div>
    );
  }
}

export default connect(
    state => state.customerItems,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(GetCustomerItems);

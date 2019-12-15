import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-select',
  templateUrl: './select.component.html',
  styleUrls: ['./select.component.css']
})
export class SelectComponent implements OnInit {
  @Input() items;
  @Input() item;
  @Output() itemChange = new EventEmitter<string>();

  onSelectedItemChange(model: string) {
    this.item = model;
    this.itemChange.emit(model);
  }

  constructor() { }

  ngOnInit() {
  }

}

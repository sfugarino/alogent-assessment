import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-home-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.css']
})
export class ToolbarComponent implements OnInit {

  @Output() createBoard = new EventEmitter<string>();

  constructor() { }

  ngOnInit() {
  }

  public createBoardClicked(name): void {
    this.createBoard.emit(name);
  }
}

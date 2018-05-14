import { TestBed, inject } from '@angular/core/testing';

import { BoardsService } from './boards.service';

describe('BoardsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BoardsService]
    });
  });

  it('should be created', inject([BoardsService], (service: BoardsService) => {
    expect(service).toBeTruthy();
  }));
});

import { TestBed } from '@angular/core/testing';

import { LocalStorateJwtService } from './local-storate-jwt.service';

describe('LocalStorateJwtService', () => {
  let service: LocalStorateJwtService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LocalStorateJwtService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
